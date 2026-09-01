
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {Provider} from "react-redux";
import {BrowserRouter} from "react-router";
import {store} from "./store";

createRoot(document.getElementById('root')!).render(
    <Provider store={store}>
        <BrowserRouter>
            <App/>
        </BrowserRouter>,
    </Provider>
)
