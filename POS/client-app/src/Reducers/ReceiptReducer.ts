import ReceiptItem from "../Models/ReceiptItem";

const initialState = {
    receiptItems: [] as ReceiptItem[],
    selectedReceiptItem: null,
    receiptTotal: null
}

export type ReceiptState = Readonly<typeof initialState>;