import ReceiptItem from "../Models/ReceiptItem";

const initialState = {
    receiptItems: [] as ReceiptItem[],
    selectedReceiptItem: null,
    receiptTotal: null,
    isLoading: false
}

export type ReceiptState = Readonly<typeof initialState>;

export default (state: ReceiptState = initialState, action: any): ReceiptState => {
    return state;
}