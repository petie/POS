import { Store, createStore, applyMiddleware, compose } from "redux";
import {AppState} from "../State/AppState";
import thunk from 'redux-thunk';
import rootReducer, { IRootState } from "../Reducers/Index";

const defaultMiddlewares = [
    thunk
  ];

const composedMiddlewares = (middlewares: any) =>
  process.env.NODE_ENV === 'development'
    ? compose(
        applyMiddleware(...defaultMiddlewares, ...middlewares)
      )
    : compose(applyMiddleware(...defaultMiddlewares, ...middlewares));

const AppStore = (initialState?: IRootState, middlewares: any = []) => createStore(rootReducer, initialState, composedMiddlewares(middlewares))
export default AppStore;