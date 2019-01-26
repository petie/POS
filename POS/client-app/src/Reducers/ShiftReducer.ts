const initialState = {
    previousAmount: null,
    startAmount: null,
    hasOpenShift: false,
    isLoading: false,
    showStartShift: false,
    showEndShift: false,
    showDialog: false,
    numberOfReceipts: 0,
    numberOfCancelledReceipts: 0,
    numberOfCancelledReceiptItems: 0,
    sales: 0,
    current: 0
    
}

export const startShift = (startAmount: any) => async (dispatch: any, getState: any) => {

};

export const initializeShift = () => async (dispatch: any, getState: any) => {

}

export const endShiftShow = () => async (dispatch: any, getState: any) => {
    
};

export const endShiftSubmit = () => async (dispatch: any, getState: any) => {
    
};

export type ShiftState = Readonly<typeof initialState>;
export default (state: ShiftState = initialState, action: any):  ShiftState => {
    switch(action.type){
        default:
            return state;
            break;
    }
}