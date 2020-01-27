import React from "react";
import LayoutSelector from "../LayoutSelector";
import { Meta } from "../../Meta";

export default function Company({
  name,
  urlPath,
  files,
  directories
}: Meta) {
  const tmp = directories && directories["TMP"];
  const notTmp =
    directories && Object.values(directories).filter(o => o.name !== "TMP");
  return (
    <>
      <div>{tmp && <LayoutSelector {...tmp} key={tmp.name} />}</div>
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
