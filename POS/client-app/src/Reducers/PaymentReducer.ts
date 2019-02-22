import Axios from "axios";
import { config } from "../config";
import { createAction } from "redux-actions";
import { SUCCESS } from "../Actions/Utils";

const initialState = {
    showPayDialog: false,
    amount: 0,
    amountPayed: 0,
    change: 0,
    isLoading: false,
    canPay: false,
    id: 0
}
const ACTIONS = {
    PAYDIALOG_SHOW: "payment/PAYDIALOG_SHOW",
    PAYDIALOG_CANCEL: "payment/PAYDIALOG_CANCEL",
    PAYDIALOG_SUBMIT: "payment/PAYDIALOG_SUBMIT"
}

export type PaymentState = Readonly<typeof initialState>;

export const payShowDialog = createAction(ACTIONS.PAYDIALOG_SHOW, (id: number) => { return Axios.post(config.apiAddress + "api/payment")});
export const payCancelDialog = createAction(ACTIONS.PAYDIALOG_CANCEL);
export const paySubmit = (id: number, amount: number) => (dispatch, getState) => {
    Axios.post(config.apiAddress + "api/payment/pay", {paymentId:id, amount}).then(response => {
        dispatch({
            type: "receipt/RESET"
        });

        dispatch({
            type: SUCCESS(ACTIONS.PAYDIALOG_SUBMIT)
        });
    })
}
//export const paySubmit = createAction(ACTIONS.PAYDIALOG_SUBMIT, (id: number, amount: number) => { return Axios.post(config.apiAddress + "api/payment/pay", {id, amount})});

export default (state: PaymentState = initialState, action:any): PaymentState => {
    switch (action.type) {
        case SUCCESS(ACTIONS.PAYDIALOG_SHOW):
            return {
                ...state,
                ...action.payload.data,
                showPayDialog: true
            };
        case SUCCESS(ACTIONS.PAYDIALOG_SUBMIT):
            return {
                ...state,
                showPayDialog: false
            }
        case ACTIONS.PAYDIALOG_CANCEL:
            return {
                ...state,
                showPayDialog: false
            }
        default:
            break;
    }
    return state;
}