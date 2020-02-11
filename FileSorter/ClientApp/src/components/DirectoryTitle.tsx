import styled from 'styled-components';
import React, { ReactElement } from 'react';
import { useRouter } from '../utils/useRouter';

interface Props {
  children: React.ReactNode;
  urlPath: string;
}

function DirectoryTitle({ urlPath, children }: Props): ReactElement {
  const router = useRouter();

  return (
    <a
      href={urlPath}
      onClick={e => {
        e.preventDefault();
        router.push(urlPath);
      }}
    >
      {children}
    </a>
  );
}

const StyledDirectoryTitle = styled(DirectoryTitle)`
  font-weight: bold;
  cursor: pointer;
`;

export default StyledDirectoryTitle;
