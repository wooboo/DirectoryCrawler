import styled from 'styled-components';
export const DirectoryEntry = styled.div`
  list-style: none;
  padding: 2px;
  margin: 2px;
  border-width: 1px;
  border-style: solid;
  border-color: gray;
  /* flex-grow: 1; */
`;
export const DirectoryListing = styled.div<{ direction?: 'row' | 'column' }>`
  padding: 2px;
  margin: 0;
  display: flex;
  flex-direction: ${props => props.direction || 'column'};
  flex-wrap: wrap;
  justify-content: flex-start;
  /* align-items: stretch; */
  /* align-content: stretch; */
`;

export const DirectoryContainer = styled.div<{ isDropping?: boolean }>`
  padding: 2px;
  opacity: ${({ isDropping }) => (isDropping ? '0.5' : '1')};
`;
