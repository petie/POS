import shift, { ShiftState } from "./ShiftReducer";
import product, { ProductState } from "./ProductReducer";
import receipt, { ReceiptState } from "./ReceiptReducer";
import payment, { PaymentState } from "./PaymentReducer";
import { combineReducers } from "redux";

export interface IRootState {
    readonly payment: PaymentState;
    readonly receipt: ReceiptState;
    readonly product: ProductState;
    readonly shift: ShiftState;
}

const rootReducer = combineReducers<IRootState>({
    payment,
    receipt,
    product,
    shift
});

export default rootReducer;