import React, { ReactElement } from 'react'
import { Properties } from '../Meta'
import Thumbnail from './Thumbnail'
import styled from 'styled-components';

interface Props {
    name: string;
    urlPath: string;
    properties: Properties;
    width: number;
    height: number;
}
const Container = styled.div<{
    width: number;
    height: number;
}>`
  position: relative;
  width: ${({ width }) => width}px;
  height: ${({ height }) => height}px;
`;
const Caption = styled.span`
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(197, 197, 197, 0.86);
  border-radius: 2px;   
  padding: 2px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
`;

function File({ name, urlPath, width, height, properties }: Props): ReactElement {
    return (
        <Container width={width} height={height}>
            <Thumbnail src={`/files${urlPath}`} height={height} />
            <Caption title={name}>{name}</Caption>
        </Container>
    )
}

export default File
