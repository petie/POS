import { Reducer } from 'redux'

const initialState = {
    previousAmount: null,
    startAmount: null
    // numberOfReceipts: number;
    // numberOfCancelledReceipts: number;
    // numberOfCancelledReceiptItems: number;
    // sales: string;
    // current: string;
}

export type ShiftState = Readonly<typeof initialState>;
export default (state: ShiftState = initialState, action: any):  ShiftState => {
    switch(action.type){
        default:
            return state;
            break;
    }
}