import { Store, createStore, applyMiddleware, compose } from "redux";
//import { composeWithDevTools } from 'redux-devtools-extension';
import {AppState} from "../State/AppState";
import promise from 'redux-promise-middleware'
import logger from "redux-logger";
import thunk from 'redux-thunk';
import rootReducer, { IRootState } from "../Reducers/Index";

// const defaultMiddlewares = [
//     thunk,
//     promise,
//     logger
//   ];

// const composedMiddlewares = (middlewares: any) => compose(applyMiddleware(...defaultMiddlewares, ...middlewares))

const AppStore = (initialState?: IRootState) => createStore(rootReducer, initialState, applyMiddleware(promise(), logger, thunk))
export default AppStore;