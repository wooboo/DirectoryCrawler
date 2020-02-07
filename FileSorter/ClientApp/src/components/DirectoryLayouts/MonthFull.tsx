import React from 'react';
import styled from 'styled-components';
import LayoutSelector from '../LayoutSelector';
import { Meta, Properties } from '../../Meta';
import { DirectoryEntry } from '../Basics';
import DirectoryEntries from '../DirectoryEntries';

const RightContainer = styled.div`
  overflow-y: auto;
  grid-area: content;
`;
const LeftContainer = styled.div`
  overflow-y: auto;
  grid-area: tmp;
  resize: horizontal;
  min-width: 120px;
  max-width: 50vw;
`;

const Container = styled.div`
  display: grid;

  grid-template:
    'tmp content' auto
    / min-content 1fr;
  height: 100%;
  grid-gap: 3px;
`;
const MonthFull = ({ urlPath, files, directories }: Meta) => {
  const tmp = directories && directories['TMP'];
  const notTmp = Object.keys(directories).reduce((object: { [name: string]: Meta<Properties> }, key) => {
    if (key !== 'TMP') {
      object[key] = directories[key];
    }
    return object;
  }, {});
  return (
    <Container>
      <LeftContainer>
        {tmp && (
          <DirectoryEntry key={'TMP'}>
            <LayoutSelector {...tmp} key={'TMP'} />
          </DirectoryEntry>
        )}
      </LeftContainer>
      <RightContainer>
        <DirectoryEntries files={files} directories={notTmp} />
      </RightContainer>
    </Container>
  );
};

export default MonthFull;
