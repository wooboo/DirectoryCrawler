import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { Provider } from './ApiContext';
import SettingsContext from './SettingsContext';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <Provider>
      <SettingsContext.Provider value={{ thumbnail: { width: 150, height: 200 }, filePreviewApi: '/files' }}>
        <App />
      </SettingsContext.Provider>
    </Provider>
  </BrowserRouter>,
  rootElement,
);

registerServiceWorker();
