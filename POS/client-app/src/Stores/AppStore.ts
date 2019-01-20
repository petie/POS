import { Store, createStore, applyMiddleware } from "redux";
import {AppState} from "../State/AppState";
import thunk from 'redux-thunk';

export const AppStore: Store<AppState> = createStore(null, applyMiddleware(thunk))