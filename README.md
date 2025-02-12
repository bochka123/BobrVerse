# INT20H | 2025 | BobrVerse

**This project is a test project for INT20H Hackaton and restricted for commercial purposes.**

## Links:

- [BobrVerse Website](https://bobrverse.fun/)

**Technologies:**

Backend:

- Platform: .NET 8
- Network: RESTFul
- Database: MS SQL Server/Entity Framework Core
- Cloud: Azure
- Authentication: OAuth/Basic
- Other: Redis Cache/Docker

Frontend:

- React/React-Redux
- HTML5/SCSS/TS

## DB Schema

```mermaid

erDiagram
    User {
        UUID Id
        string Email
        string PasswordHash
    }

    Role {
        UUID Id
        string Name
    }

    BobrProfile {
        UUID Id
        string Name
        UUID UserId
        UUID LevelId
        int XP
        int Logs
        string Url
    }

    BobrLevel {
        UUID Id
        int Level
        int RequiredXP
        string Title
        string Description
    }

    Quest {
        UUID Id
        string Title
        string Description
        UUID AuthorId
        DateTime CreatedAt
        DateTime UpdatedAt
        int XpForComplete
        int XpForSuccess
        QuestStatusEnum Status
        TimeSpan TimeLimit
        string Url
    }

    QuestResponse {
        UUID Id
        UUID QuestId
        UUID ProfileId
        string QuestTitle
        string QuestDescription
        int XpEarned
        QuestResponseStatusEnum Status
        int TotalXp
        DateTime CompletedAt
        DateTime StartedAt
        UUID QuestRatingId
    }

    QuestRating {
        UUID Id
        UUID QuestId
        UUID BobrProfileId
        UUID QuestResponseId
        DateTime CreatedAt
        string Comment
        int Rating
    }

    QuizTask {
        UUID Id
        UUID QuestId
        TaskTypeEnum TaskType
        string Url
        string ShortDescription
        string Description
        bool IsRequiredForNextStage
        int MaxAttempts
        TimeSpan TimeLimit
        bool IsTemplate
        int Order
    }

    QuizTaskStatus {
        UUID Id
        UUID QuizTaskId
        UUID QuestResponseId
        TaskTypeEnum TaskType
        QuestTaskStatusEnum Status
        int CurrentAttempt
        int EarnedXp
        DateTime CompletedAt
    }

    Resource {
        UUID Id
        ResourceNameEnum Name
        int Quantity
        int Length
        int Weight
    }

    BobrProfile ||--|{ Quest : "CreatedQuests"
    BobrProfile ||--|{ QuestResponse : "QuestResponses"
    BobrProfile ||--|{ QuestRating : "QuestRatings"
    BobrProfile }|--|| User : "belongsTo"
    BobrProfile }|--|| BobrLevel : "hasLevel"

    Quest ||--|{ QuestResponse : "Responses"
    Quest ||--|{ QuestRating : "Ratings"
    Quest ||--|{ QuizTask : "Tasks"

    QuestResponse ||--|{ QuizTaskStatus : "TaskStatuses"
    QuestResponse ||--|| Quest : "belongsTo"
    QuestResponse ||--|| BobrProfile : "belongsTo"
    QuestResponse }|--|| QuestRating : "hasOne"

    QuestRating ||--|| Quest : "rates"
    QuestRating ||--|| BobrProfile : "byUser"
    QuestRating ||--|| QuestResponse : "forResponse"

    QuizTask ||--|{ QuizTaskStatus : "TaskStatuses"
    QuizTask ||--|{ Resource : "RequiredResources"
    QuizTask ||--|| Quest : "belongsTo"

    QuizTaskStatus ||--|| QuizTask : "task"
    QuizTaskStatus ||--|| QuestResponse : "response"

    User ||--o{ Role : "hasRoles"
    
```