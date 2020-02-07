import React from 'react';
const SettingsContext = React.createContext({ thumbnail: { width: 150, height: 200 }, filePreviewApi: '/files' });
export default SettingsContext;
