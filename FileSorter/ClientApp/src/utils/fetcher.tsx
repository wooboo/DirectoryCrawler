import fetch from "unfetch";
export default (url: string) => fetch(url).then(r => r.json());
