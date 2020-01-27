import React from "react";
import { useRouter } from "../../utils/useRouter";
import { Meta } from "../../Meta";

export default function CompanyList({
  name,
  urlPath,
  files,
  directories
}: Meta) {
  const router = useRouter();
  const companies = Object.values(directories);
  
  return (
    <ul>{companies && companies.map(n => <h1 onClick={()=>router.push(n.urlPath)} key={n.name}>{n.name}</h1>)}</ul>
  );
}
