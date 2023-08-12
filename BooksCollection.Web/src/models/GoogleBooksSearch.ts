import { Book } from "./Book";
import { BaseApiResponse } from "./BaseApi";

export class GoogleBooksSearchResponse extends BaseApiResponse {
  books: Book[] = []; 
}
