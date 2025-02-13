import { ReactNode } from 'react';

const wordsRegex = /(\w+|\W+)/g;

const highlight = (code: string, objectKeywords: string[], commandKeywords: string[]): ReactNode => {
    return code
        .split(wordsRegex)
        .map((word) => {
            if (objectKeywords.includes(word)) {
                return `<span style="color: greenyellow;">${word}</span>`;
            }
            if (commandKeywords.includes(word)) {
                return `<span style="color: gold;">${word}</span>`;
            }
            return word;
        })
        .join('');
};

export { highlight };
