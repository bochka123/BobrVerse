const getFormErrorMessage = (error: object): string => {
    for (const [_, value] of Object.entries(error)) {
        if (value && ('message' in value) && value.message) {
            return value.message;
        }
    }
    return 'Failed form validation';
};

export { getFormErrorMessage };
