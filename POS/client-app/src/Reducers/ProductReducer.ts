import Product from "../Models/Product";
import axios from "axios";
import { config } from "../config";
import { createAction } from 'redux-actions';
import { FETCHING, FAILURE, SUCCESS } from "../Actions/Utils";
import { ProductApi } from "../API";

const Api = new ProductApi({});
const initialState = {
    products: [] as Product[],
    showProductDialog: false,
    isLoading: false,
    errorMessage: "",
    selectedProduct: "",
    canSearchProduct: true
}

export const ACTIONS = {
    FETCH_PRODUCTS: 'products/FETCH_PRODUCTS',
    SHOW_PRODUCTS_DIALOG: 'products/SHOW_PRODUCTS_DIALOG',
    PICK_PRODUCT: 'products/PICK_PRODUCT',
    CLOSE_PRODUCTS_DIALOG: 'products/CLOSE_PRODUCTS_DIALOG'
}

export type ProductState = Readonly<typeof initialState>;

export default (state: ProductState = initialState, action: any): ProductState => {
    switch (action.type) {
        case FETCHING(ACTIONS.FETCH_PRODUCTS):
            return {
                ...state,
                isLoading: true
            }
            break;
        case SUCCESS(ACTIONS.FETCH_PRODUCTS):
            return {
                ...state,
                products: action.payload.data,
                isLoading: false
            }
            break;
        case FAILURE(ACTIONS.FETCH_PRODUCTS):
            return {
                ...state,
                errorMessage: action.payload,
                isLoading: false
            }
            break;
        case ACTIONS.SHOW_PRODUCTS_DIALOG:
            return {
                ...state,
                showProductDialog: true
            }
            break;
        case ACTIONS.CLOSE_PRODUCTS_DIALOG:
            return {
                ...state,
                showProductDialog: false
            }
            break;
        case ACTIONS.PICK_PRODUCT:
            return {
                ...state,
                showProductDialog: false,
                selectedProduct: action.payload
            }
            break;
        default:
            break;
    }
    return state;
}

export const productFetch = createAction(ACTIONS.FETCH_PRODUCTS, () => { return axios.get(config.apiAddress + "api/product")})
export const productDialogClose = createAction(ACTIONS.CLOSE_PRODUCTS_DIALOG);
export const productDialogShow = createAction(ACTIONS.SHOW_PRODUCTS_DIALOG);
//export const productFetch = createAction(ACTIONS.FETCH_PRODUCTS, Api.productGetAll)
// export const productFetch = () => {
//     return {
//         type: ACTIONS.FETCH_PRODUCTS,
//         payload: axios.get(config.apiAddress + "api/product")
//     };
// }
//export const pickProduct = createAction(ACTIONS.PICK_PRODUCT, (ean: string) => ean);
// export const pickProduct = (ean: string) => {
//     return {
//         type: ACTIONS.PICK_PRODUCT,
//         payload: ean
//     }
// }

// export const productDialogShow = () => {
//     return {
//         type: ACTIONS.SHOW_PRODUCTS_DIALOG
//     };
// }
