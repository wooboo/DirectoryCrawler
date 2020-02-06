import fetch from 'unfetch';
export default (url: string, method: 'POST' | 'PUT', data: any) =>
    fetch(url, {
        method,
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
