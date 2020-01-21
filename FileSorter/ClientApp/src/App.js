import React, { useState } from 'react';
import { DndProvider } from 'react-dnd'
import useSWR, { trigger } from "swr";
import Backend from 'react-dnd-html5-backend';
import './custom.css'
import ThumbnailSizeContext from './ThumbnailSizeContext';
import fetcher from "./utils/fetcher";
import LayoutSelector from './components/LayoutSelector';
import {useRouter} from './utils/useRouter';
export default () => {
  const [size, setSize] = useState(100);
  const router = useRouter();
  const { data } = useSWR(`/directories${router.pathname}`, fetcher);
  return (
    <>
      <ThumbnailSizeContext.Provider value={size}>
        <DndProvider backend={Backend}>
          {data &&
            <LayoutSelector {...data} />
          }
        </DndProvider>
      </ThumbnailSizeContext.Provider>
    </>
  );
}
