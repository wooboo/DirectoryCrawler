import React from "react";
import { useRouter } from "../../utils/useRouter";

export default function Company({
  name,
  relativePath,
  files,
  directories
}: {
  name: string;
  relativePath: string;
  files: any;
  directories: any;
}) {
  const router = useRouter();
  const companies = Object.values(directories);
  
  return (
    <ul>{companies && companies.map(n => <h1 onClick={()=>router.push(n.relativePath)} key={n.name}>{n.name}</h1>)}</ul>
  );
}
