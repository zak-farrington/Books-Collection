import axios from "axios";
import appConfig from "../appConfig";
import { AddBookRequest, AddBookResponse } from "../models/AddBook";
import { BooksListResponse } from "../models/BookList";
import { ModifyBookRequest, ModifyBookResponse } from "../models/ModifyBook";
import { DeleteBookResponse } from "../models/DeleteBook";
import { GoogleBooksSearchResponse } from "../models/GoogleBooksSearch";

class ApiService {
    ApiUrlBase!: string;

    constructor() {
        if (appConfig.apiUrl?.length > 0) {
            this.ApiUrlBase = appConfig.apiUrl;
        } else {
            throw new Error("Cannot load Books API URL from config.");
        }
    }

    async getBooksList(): Promise<BooksListResponse> {
        const url = `${this.ApiUrlBase}/books/list`;
        const response = await axios.get(url);
        return response.data;
    }

    async addBook(request: AddBookRequest): Promise<AddBookResponse> {
        const url = `${this.ApiUrlBase}/books/add`;
        const response = await axios.post(url, request);
        return response.data;
    }

    async modifyBook(request: ModifyBookRequest): Promise<ModifyBookResponse> {
        const url = `${this.ApiUrlBase}/books/modify`;
        const response = await axios.patch(url, request);
        return response.data;
    }

    async deleteBook(uid: string): Promise<DeleteBookResponse> {
        const url = `${this.ApiUrlBase}/books/delete?uid=${encodeURIComponent(uid)}`;
        const response = await axios.delete(url);
        return response.data;
    }

    async searchGoogleBooks(title: string): Promise<GoogleBooksSearchResponse> {
        const url = `${this.ApiUrlBase}/google-books/search?title=${encodeURIComponent(title)}`;
        const response = await axios.get(url);
        return response.data;
    }
}

const apiServiceToExport = new ApiService();
export default apiServiceToExport;
