﻿using AutoMapper;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Common.Models.DTO.File;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services
{
    public class BobrAccountService(BobrVerseContext context, IUserContextService userContextService, IMapper mapper, IAzureBlobStorageService azureBlobStorageService) : IBobrAccountService
    {
        public IQueryable<BobrProfile> BobrProfilesQueryable =>
            context.BobrProfiles.Include(x => x.Level).AsNoTracking();

        public async Task<MyBobrProfileDTO?> GetMyProfileAsync()
        {
            var profile = await BobrProfilesQueryable.FirstOrDefaultAsync(x => x.UserId == userContextService.UserId);
            return profile == null ? null : mapper.Map<MyBobrProfileDTO>(profile);
        }

        public async Task EnsureProfileCreatedAsync()
        {
            var userId = userContextService.UserId;

            if (await context.BobrProfiles.AnyAsync(x => x.UserId == userId))
            {
                return;
            }

            var defaultLevel = await context.BobrLevels.AsNoTracking().OrderBy(x => x.Level).FirstOrDefaultAsync()
                ?? throw new BobrException("No levels configured.");

            var newProfile = new BobrProfile
            {
                UserId = userId,
                Name = $"bobr {Guid.NewGuid().ToString()[..8]}",
                LevelId = defaultLevel.Id,
                XP = 0,
                Logs = 10
            };

            context.BobrProfiles.Add(newProfile);
            await context.SaveChangesAsync();
        }

        public async Task<MyBobrProfileDTO> UpdateProfileAsync(UpdateBobrProfileDTO dto)
        {
            var userId = userContextService.UserId;
            var profile = await context.BobrProfiles.Include(x => x.Level).FirstOrDefaultAsync(x => x.UserId == userId)
                ?? throw new BobrException("Profile doesn't exists.");

            mapper.Map(dto, profile);
            await context.SaveChangesAsync();
            return mapper.Map<MyBobrProfileDTO>(profile);
        }

        public async Task<FileDto> UploadPhotoAsync(IFormCollection formCollection)
        {
            var file = formCollection.Files.FirstOrDefault();
            var newFileDto = new NewFileDto()
            {
                Stream = file.OpenReadStream(),
                FileName = file.FileName
            };
            var userId = userContextService.UserId;
            var profile = await context.BobrProfiles.Include(x => x.Level).FirstOrDefaultAsync(x => x.UserId == userId)
                ?? throw new InvalidOperationException("Profile doesn't exists.");

            if (!string.IsNullOrEmpty(profile.Url))
            {
                var oldFile = new FileDto()
                {
                    Url = profile.Url
                };

                await azureBlobStorageService.DeleteFromBlob(oldFile);
            }

            var fileDto = await azureBlobStorageService.AddFileToBlobStorage(newFileDto);

            profile.Url = fileDto.Url;
            await context.SaveChangesAsync();
            return fileDto;
        }

        public async Task<bool> DeletePhotoAsync()
        {
            var userId = userContextService.UserId;
            var profile = await context.BobrProfiles.Include(x => x.Level).FirstOrDefaultAsync(x => x.UserId == userId)
                ?? throw new InvalidOperationException("Profile doesn't exists.");

            var oldFile = new FileDto()
            {
                Url = profile.Url
            };

            await azureBlobStorageService.DeleteFromBlob(oldFile);

            return true;
        }

        public async Task AddXPAsync(int xp, bool save = false)
        {
            var profile = await context.BobrProfiles.FirstAsync(x => x.UserId == userContextService.UserId);
            profile.XP += xp;

            var nextLevel = await context.BobrLevels
                .Where(l => l.RequiredXP <= profile.XP)
                .OrderByDescending(l => l.Level)
                .FirstOrDefaultAsync();

            if (nextLevel != null && nextLevel.Id != profile.LevelId)
            {
                profile.LevelId = nextLevel.Id;
            }

            if (save)
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
