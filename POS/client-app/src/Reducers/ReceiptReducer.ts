import ReceiptItem from "../Models/ReceiptItem";
import { config } from "../config";
import axios from "axios";
import { createAction } from "redux-actions";
import { SUCCESS } from "../Actions/Utils";
import { IRootState } from "./Index";
import Product from "../Models/Product";

const initialState = {
    items: [] as ReceiptItem[],
    selectedReceiptItem: -1,
    selectedReceiptItemQuantity: 1,
    receiptTotal: "0 zł",
    isLoading: false,
    canCancelReceipt: false,
    canRemoveItem: false,
    canChangeQuantity: false,
    canPay: false,
    id: 0,
    isCancelled: false,
    isClosed: false,
    isOpen: false,
    showQuantityDialog: false,
    showReceiptCancelDialog: false,
    total: 0
};

const ACTIONS = {
    ADD_ITEM: "receipt/ADD_ITEM",
    CREATERECEIPT_SUBMIT: "receipt/CREATERECEIPT_SUBMIT",
    GETCURRENTRECEIPT: "receipt/GETCURRENTRECEIPT",
    REMOVE_ITEM: "receipt/REMOVE_ITEM",
    DELETERECEIPT_SUBMIT: "receipt/DELETERECEIPT_SUBMIT",
    DELETERECEIPT_SHOW: "receipt/DELETERECEIPT_SHOW",
    CHANGEQUANTITY_SUBMIT: "receipt/CHANGEQUANTITY_SUBMIT",
    CHANGEQUANTITY_SHOW: "receipt/CHANGEQUANTITY_SHOW",
    SELECTRECEIPTITEM: "receipt/SELECTRECEIPTITEM",
    CHANGEQUANTITY_CANCEL: "receipt/CHANGEQUANTITY_CANCEL",
    DELETERECEIPT_CANCEL: "receipt/DELETERECEIPT_CANCEL",
    SELECTNEXTRECEIPTITEM: "receipt/SELECTNEXTRECEIPTITEM",
    SELECTPREVRECEIPTITEM: "receipt/SELECTPREVRECEIPTITEM",
    RESET: "receipt/RESET"
};
export const deleteReceiptCancel = createAction(ACTIONS.DELETERECEIPT_CANCEL);
export const deleteReceiptShow = createAction(ACTIONS.DELETERECEIPT_SHOW);
export const changeQuantityShow = createAction(ACTIONS.CHANGEQUANTITY_SHOW);
export const changeQuantityCancel = createAction(ACTIONS.CHANGEQUANTITY_CANCEL);
export const selectReceiptItem = createAction(ACTIONS.SELECTRECEIPTITEM, (id: number) => id);
export const selectNextReceiptItem = createAction(ACTIONS.SELECTNEXTRECEIPTITEM);
export const selectPrevReceiptItem = createAction(ACTIONS.SELECTPREVRECEIPTITEM);
export const createReceiptSubmit = createAction(ACTIONS.CREATERECEIPT_SUBMIT, () => {
    return axios.post(config.apiAddress + "api/receipt");
});
export const getCurrentReceipt = createAction(ACTIONS.GETCURRENTRECEIPT, () => {
    return axios.get(config.apiAddress + "api/receipt");
});
export const removeItemFromReceipt = createAction(ACTIONS.REMOVE_ITEM, (receiptId: number, receiptItemId: number) => {
    return axios.delete(config.apiAddress + "api/receipt/" + receiptId + "/" + receiptItemId);
});
// export const addItemToReceipt = createAction(ACTIONS.ADD_ITEM, (ean: string)
// => {     return axios.post(config.apiAddress + "api/receipt/" + ean); });
export const addItemToReceipt = (ean: string) => (dispatch, getState: () => IRootState) => {
    var products = getState().product.products;
    var found =
        products.findIndex((element: Product) => {
            return element.ean == ean;
        }) > -1;
    if (found) {
        dispatch({
            type: ACTIONS.ADD_ITEM,
            payload: axios.post(config.apiAddress + "api/receipt/" + ean)
        });
    } else {
        dispatch({ type: "products/SHOW_PRODUCTS_DIALOG" });
    }
};
export const deleteReceiptSubmit = createAction(ACTIONS.DELETERECEIPT_SUBMIT, (receiptId: number) => {
    return axios.delete(config.apiAddress + "api/receipt/" + receiptId);
});
export const changeQuantitySubmit = createAction(ACTIONS.CHANGEQUANTITY_SUBMIT, (id: number, quantity: number) => {
    return axios.post(config.apiAddress + "api/receipt/change", {
        id: id,
        quantity: quantity
    });
});

export type ReceiptState = Readonly<typeof initialState>;

export default (state: ReceiptState = initialState, action: any): ReceiptState => {
    switch (action.type) {
        case ACTIONS.SELECTPREVRECEIPTITEM:
            var selectedIndex = state.items.findIndex((item: ReceiptItem) => item.id === state.selectedReceiptItem);
            var newIndex = selectedIndex;
            if (selectedIndex > 0) newIndex--;
            return {
                ...state,
                selectedReceiptItem: newIndex < 0 ? newIndex : state.items[newIndex].id
            };
            break;
        case ACTIONS.SELECTNEXTRECEIPTITEM:
            var selectedIndex = state.items.findIndex((item: ReceiptItem) => item.id === state.selectedReceiptItem);
            var newIndex = selectedIndex;
            if (selectedIndex < state.items.length - 1) newIndex++;
            return {
                ...state,
                selectedReceiptItem: newIndex < 0 ? newIndex : state.items[newIndex].id
            };
            break;
        case ACTIONS.CHANGEQUANTITY_SHOW:
            const item = state.items.find((item: ReceiptItem) => {
                return item.id == state.selectedReceiptItem;
            });
            const q = item ? item.quantity : 1;
            return {
                ...state,
                showQuantityDialog: true,
                selectedReceiptItemQuantity: q
            };
        case ACTIONS.RESET:
            return {
                ...initialState
            };
        case ACTIONS.DELETERECEIPT_SHOW:
            return {
                ...state,
                showReceiptCancelDialog: true
            };
        case ACTIONS.DELETERECEIPT_CANCEL:
            return {
                ...state,
                showReceiptCancelDialog: false
            };
        case ACTIONS.CHANGEQUANTITY_CANCEL:
            return {
                ...state,
                showQuantityDialog: false
            };
        case ACTIONS.SELECTRECEIPTITEM:
            return {
                ...state,
                selectedReceiptItem: action.payload
            };
        case SUCCESS(ACTIONS.DELETERECEIPT_SUBMIT):
            return {
                ...initialState
            };
        case SUCCESS(ACTIONS.CREATERECEIPT_SUBMIT):
            return {
                ...state,
                ...action.payload.data
            };
        case SUCCESS(ACTIONS.GETCURRENTRECEIPT):
            var receipt = action.payload.data as ReceiptState;

            var selectedReceiptItem = receipt.items && receipt.items.length > 0 ? receipt.items[receipt.items.length - 1].id : null;
            return {
                ...state,
                ...action.payload.data,
                selectedReceiptItem
            };
        case SUCCESS(ACTIONS.CHANGEQUANTITY_SUBMIT):
            return {
                ...state,
                ...action.payload.data,
                showQuantityDialog: false
            };
        case SUCCESS(ACTIONS.ADD_ITEM):
            var receipt = action.payload.data as ReceiptState;
            var selectedReceiptItem = receipt.items && receipt.items.length > 0 ? receipt.items[receipt.items.length - 1].id : null;
            return {
                ...state,
                ...action.payload.data,
                selectedReceiptItem
            };
        case SUCCESS(ACTIONS.REMOVE_ITEM):
            var receipt = action.payload.data as ReceiptState;
            var selectedReceiptItem = receipt.items && receipt.items.length > 0 ? receipt.items[receipt.items.length - 1].id : null;
            return {
                ...state,
                ...action.payload.data,
                selectedReceiptItem
            };

        default:
            break;
    }
    return state;
};
