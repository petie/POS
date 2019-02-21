import { createAction } from "redux-actions";
import Axios from "axios";
import { config } from "../config";
import { SUCCESS, FAILURE } from "../Actions/Utils";

const initialState = {
    endMoney: 0,
    startMoney: 0,
    startDeposit: 0,
    numberOfReceipts: 0,
    cancelledReceiptsCount: 0,
    removedItemsCount: 0,
    sales: 0,
    isCreated: false,
    isOpen: false,
    isLoading: false,
    showStartShift: false,
    showEndShift: false,
    showDialog: false,
    canOpenShift: true,
    canCloseShift: false,
    id: 0
};

const ACTIONS = {
    STARTSHIFT_SHOW: "shift/STARTSHIFT_SHOW",
    GETCURRENTSHIFT: "shift/GETCURRENTSHIFT",
    CLOSESHIFT_SHOW: "shift/CLOSESHIFT_SHOW",
    CLOSESHIFT_CANCEL: "shift/CLOSESHIFT_CANCEL",
    CLOSESHIFT_SUBMIT: "shift/CLOSESHIFT_SUBMIT",
    STARTSHIFT_SUBMIT: "shift/STARTSHIFT_SUBMIT",
    STARTSHIFT_CANCEL: "shfit/STARTSHIFT_CANCEL",
    STARTSHIFTEXISTING_SHOW: "shift/STARTSHIFTEXISTING_SHOW"
};

export const startShiftShow = createAction(ACTIONS.STARTSHIFT_SHOW, () => {
    return Axios.post(config.apiAddress + "api/shift");
});
export const getCurrentShift = createAction(ACTIONS.GETCURRENTSHIFT, () => {
    return Axios.get(config.apiAddress + "api/shift");
});

export const startShiftSubmit = createAction(ACTIONS.STARTSHIFT_SUBMIT, (id: number, depositAmount: number) => {
    return Axios.post(config.apiAddress + "api/shift/start", { shiftId: id, depositAmount: depositAmount });
});

export const startShiftShowCancel = createAction(ACTIONS.STARTSHIFT_CANCEL);
export const endShiftShow = createAction(ACTIONS.CLOSESHIFT_SHOW);
export const endShiftShowCancel = createAction(ACTIONS.CLOSESHIFT_CANCEL);
export const endShiftSubmit = createAction(ACTIONS.CLOSESHIFT_SUBMIT, () => {
    return Axios.post(config.apiAddress + "api/shift/close");
});
export const startShiftExistingShow = createAction(ACTIONS.STARTSHIFTEXISTING_SHOW);

export type ShiftState = Readonly<typeof initialState>;
export default (state: ShiftState = initialState, action: any): ShiftState => {
    switch (action.type) {
        case SUCCESS(ACTIONS.CLOSESHIFT_SUBMIT):
            return {
                ...state,
                ...action.payload.data,
                showDialog: false,
                canOpenShift: true
            };
        case ACTIONS.CLOSESHIFT_CANCEL:
        case ACTIONS.STARTSHIFT_CANCEL:
            return {
                ...state,
                showDialog: false
            };
        case SUCCESS(ACTIONS.STARTSHIFT_SUBMIT):
            return {
                ...state,
                ...action.payload.data,
                canOpenShift: false,
                canCloseShift: true,
                showDialog: false
            };
        case SUCCESS(ACTIONS.GETCURRENTSHIFT):
            const shift = action.payload.data;
            return {
                ...state,
                ...shift,
                canCloseShift: shift.isOpen,
                canOpenShift: shift.isCreated && !shift.isOpen
            };
        case FAILURE(ACTIONS.GETCURRENTSHIFT):
            return {
                ...state,
                isOpen: false,
                canOpenShift: true
            };
        case SUCCESS(ACTIONS.STARTSHIFT_SHOW):
            return {
                ...state,
                ...action.payload.data,
                showDialog: true,
                showStartShift: true,
                showEndShift: false
            };
        case ACTIONS.STARTSHIFTEXISTING_SHOW:
            return {
                ...state,
                showDialog: true,
                showStartShift: true,
                showEndShift: false
            };
        case ACTIONS.CLOSESHIFT_SHOW:
            return {
                ...state,
                showDialog: true,
                showEndShift: true,
                showStartShift: false
            };
        default:
            return state;
    }
};
