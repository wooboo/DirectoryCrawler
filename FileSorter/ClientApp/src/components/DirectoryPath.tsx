import React, { ReactElement } from 'react';
import StyledDirectoryTitle from './DirectoryTitle';

interface Props {
  urlPath: string;
}

function DirectoryPath({ urlPath }: Props): ReactElement {
  const elements = urlPath.split('/');
  const items: Array<{ name: string; urlPath: string }> = [];
  let last = '';
  for (let index = 1; index < elements.length; index++) {
    const element = elements[index];
    last = `${last}/${element}`;
    items.push({ name: element, urlPath: last });
  }
  return (
    <span>
      {items.map(({ name, urlPath }) => (
        <>
          /
          <StyledDirectoryTitle key={urlPath} urlPath={urlPath}>
            {name}
          </StyledDirectoryTitle>
        </>
      ))}
    </span>
  );
}

export default DirectoryPath;
