export default (api: string, files: string[], onProgress?: (loaded: number, total: number) => void) => {
    return new Promise((resolve, reject) => {
        // Create Form Data
        const payload = new FormData();

        for (const file of files) {
            payload.append('files', file);
        }

        // XHR - New XHR Request
        const xhr = new XMLHttpRequest();

        xhr.onload = (...params) => {
            console.log(...params);

            resolve(...params);
        };
        // XHR - Upload Progress
        xhr.upload.onprogress = e => {
            onProgress && onProgress(e.loaded, e.total);
        };

        // XHR - Make Request
        xhr.open('POST', api);
        xhr.send(payload);
    });
};
