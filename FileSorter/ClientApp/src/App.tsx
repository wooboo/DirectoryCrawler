import React, { useContext } from 'react';
import { DndProvider } from 'react-dnd';
import styled from 'styled-components';
import Backend from 'react-dnd-html5-backend';
import './custom.css';
import LayoutSelector from './components/LayoutSelector';
import DirectoryPath from './components/DirectoryPath';
import ApiContext from './ApiContext';

const Container = styled.div`
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  flex-wrap: nowrap;
`;
const Header = styled.div`
  flex-shrink: 0;
  padding: 10px;
`;
const Content = styled.div`
  flex-grow: 1;
  overflow: auto;
  min-height: 2em;
`;
export default function App(): JSX.Element {
  const { path, data } = useContext(ApiContext);
  return (
    <Container>
      <Header>
        <DirectoryPath urlPath={path ?? ''} />
      </Header>
      <Content>
        <DndProvider backend={Backend}>{data && <LayoutSelector {...data} />}</DndProvider>
      </Content>
    </Container>
  );
}
