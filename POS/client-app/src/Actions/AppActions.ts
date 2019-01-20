import { Action } from "redux";
import ActionTypes from "./ActionTypes";
import Shift from "../Models/Shift";
import Product from "../Models/Product";

export interface StartShiftAction extends Action<ActionTypes.START_SHIFT> {
    startAmount: number;
}

export interface StartShiftShowAction extends Action<ActionTypes.START_SHIFT_SHOW> {
    currentAmount: number;
}

export interface EndShiftShowAction extends Action<ActionTypes.END_SHIFT_SHOW> {
    shift: Shift;
}

export interface AddProductAction extends Action<ActionTypes.ADD_PRODUCT> {
    ean: string;
}

export interface ChangeQuantityAction extends Action<ActionTypes.CHANGE_QUANTITY> {
    receiptItemId: number;
    newQuantity: string;
}

export interface RemoveItemAction extends Action<ActionTypes.REMOVE_ITEM> {
    receiptItemId: number;
}

export interface LoadProductsSuccessAction extends Action<ActionTypes.LOAD_PRODUCTS_SUCCESS> {
    products: Product[];
}

export interface ErrorAction<T = Action> extends Action<T> {
    errorMessage: string;
}

export interface PayAction extends Action<ActionTypes.PAY> {
    amount: string;
}

