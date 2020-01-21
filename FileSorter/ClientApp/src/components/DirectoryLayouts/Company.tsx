import React from "react";
import LayoutSelector from "../LayoutSelector";

export default function CompanyList({
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
  const tmp = directories && directories["TMP"];
  const notTmp = directories && Object.values(directories).filter(o => o.name !== "TMP");
  return (
    <>
      <div>{tmp && <LayoutSelector {...tmp} />}</div>
      <ul>
        {notTmp &&
          notTmp.map(n => (
            <li key={n.name}>
              <LayoutSelector {...n} key={n.name} />
            </li>
          ))}
      </ul>
    </>
  );
}
