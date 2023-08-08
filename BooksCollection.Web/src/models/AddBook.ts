import { Book } from "./Book"; // Assuming Book class is defined in Book.ts
import { BaseApiResponse } from "./BaseApi";

// TODO: Decide which pattern we will follow
// export class AddBookRequest {
//     constructor(public book: Book) {}
// }
  
export class AddBookRequest {
    book!: Book;
}
  
export class AddBookResponse extends BaseApiResponse {}
