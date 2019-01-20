import { Reducer, Action } from "redux";
import {AppState} from "../State/AppState";
import { EndShiftShowAction, AddProductAction } from "../Actions/AppActions";
import ActionTypes from "../Actions/ActionTypes";

export const productReducer: Reducer<AppState | undefined, AddProductAction> = (state, action) => {
    switch (action.type) {
        case ActionTypes.ADD_PRODUCT:
            return state;
            break;
    
        default:
            return state;
            break;
    }
} 