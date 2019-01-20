import Product from "../Models/Product";

const initialState = {
    products: [] as Product[],
    showProductDialog: false
}

export type ProductState = Readonly<typeof initialState>;