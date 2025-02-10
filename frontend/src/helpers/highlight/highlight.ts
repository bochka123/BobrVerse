import { ReactNode } from 'react';

const wordsRegex = /(\w+|\W+)/g;

const highlight = (code: string): ReactNode => {
    const objectKeywords = ['tree', 'beaver'];
    const commandKeywords = ['go', 'rotate', 'walk', 'run'];

    return code
        .split(wordsRegex)
        .map((word) => {
            if (objectKeywords.includes(word)) {
                return `<span style="color: white;">${word}</span>`;
            }
            if (commandKeywords.includes(word)) {
                return `<span style="color: hotpink;">${word}</span>`;
            }
            return word;
        })
        .join('');
};

export { highlight };
