import React, { createContext, ReactElement } from 'react';
import { useRouter } from './utils/useRouter';
import fetcher from './utils/fetcher';
import pusher from './utils/pusher';
import useSWR, { trigger } from 'swr';
import uploadFiles from './utils/uploadFiles';
import { Properties, Meta } from './Meta';
interface ApiContextModel {
  path: string | null;
  data: Meta<Properties> | null;
  uploadFiles: (urlPath: string, files: Array<string>) => Promise<unknown>;
  moveFiles: (urlPath: string, files: Array<string>) => Promise<unknown>;
  reloadAll: () => void;
}
interface Props {
  children: ReactElement;
}
const ApiContext = createContext<ApiContextModel>({
  data: null,
  path: null,
  uploadFiles: async (urlPath, files) => {},
  moveFiles: async (urlPath, files) => {},
  reloadAll: () => {
    console.log('no');
  },
});
export function Provider({ children }: Props) {
  const router = useRouter();
  const directoryPath = `/directories${router.pathname}`;
  const { data } = useSWR(directoryPath, fetcher);
  const model: ApiContextModel = {
    path: router.pathname,
    data,
    uploadFiles: (urlPath, files) => uploadFiles(`/files${urlPath}`, files),
    moveFiles: (urlPath, files) => pusher(`/files${urlPath}`, 'PUT', files),
    reloadAll: () => trigger(directoryPath),
  };
  return <ApiContext.Provider value={model}>{children}</ApiContext.Provider>;
}
export default ApiContext;
