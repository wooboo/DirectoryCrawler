import React from 'react';
import File from './File';
import SettingsContext from '../SettingsContext';

export default { title: 'Components/File' };
const src = 'iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==';
export const simple = () => (
    <SettingsContext.Provider value={{ thumbnailSize: 200, filePreviewApi: 'data:image/png;base64,' }}>
        <File urlPath={src} height={200} width={200} name="some file" />
    </SettingsContext.Provider>
);
