
export default (api: string, files: string[]) => {
  return new Promise((resolve, reject) => {
    // Create Form Data
    const payload = new FormData();

    for (const file of files) {
      payload.append("files", file);
    }

    // XHR - New XHR Request
    const xhr = new XMLHttpRequest();
    // XHR - Make Request
    xhr.open("POST", api);
    xhr.send(payload);
    xhr.onload = (...params) => resolve(...params);
  });
};
