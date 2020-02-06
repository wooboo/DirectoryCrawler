import React from 'react';
import styled from 'styled-components';
import LayoutSelector from '../LayoutSelector';
import { Meta, Properties } from '../../Meta';
import { DirectoryEntry } from '../Basics';
import DirectoryEntries from '../DirectoryEntries';
import DirectoryPath from '../DirectoryPath';

const RightContainer = styled.div`
  overflow-y: auto;
  grid-area: content;
`;
const LeftContainer = styled.div`
  overflow-y: auto;
  grid-area: tmp;
`;

const Container = styled.div`
  display: grid;
  grid-template-columns: 35% auto;
  grid-template-rows: 0fr auto;
  grid-template-areas:
    'header header'
    'tmp content';
  height: 100%;
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
      <DirectoryPath urlPath={urlPath} />
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
