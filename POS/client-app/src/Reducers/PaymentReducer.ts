const initialState = {
    showPayDialog: false,
    payAmount: null,
    isLoading: false
}

export type PaymentState = Readonly<typeof initialState>;

export default (state: PaymentState = initialState, action:any): PaymentState => {
    return state;
}