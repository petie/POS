import { ShiftState } from "./ShiftReducer";
import { ReceiptState } from "./ReceiptReducer";
import { ProductState } from "./ProductReducer";
import { PaymentState } from "./PaymentReducer";

export interface IRootState {
    readonly payment: PaymentState;
    readonly receipt: ReceiptState;
    readonly product: ProductState;
    readonly shift: ShiftState;
}