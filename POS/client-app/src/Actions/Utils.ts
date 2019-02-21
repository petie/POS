import { REJECTED, PENDING, FULFILLED } from "redux-promise-middleware";

export const FETCHING = (a: string) => {
    return a + "_" + PENDING;
}

export const FAILURE = (a: string) => {
    return a + "_" + REJECTED;
}

export const SUCCESS = (a: string) => {
    return a + "_" + FULFILLED;
}