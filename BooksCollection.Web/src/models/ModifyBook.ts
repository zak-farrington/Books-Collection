import { Book } from "./Book";
import { BaseApiResponse } from "./BaseApi";

export class ModifyBookRequest {
    book!: Book;
}

export class ModifyBookResponse extends BaseApiResponse {}
