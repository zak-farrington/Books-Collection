import React, { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useSelector } from "react-redux";
import { addBook, getBooksList, searchGoogleBooks, selectGoogleSearchBooks } from "../../store/booksSlice";
import { Book, BookCategory, defaultBook } from "../../models/Book";
import { useAppDispatch } from "../../store/store";
import "./AddModifyBook.less";
import { bookThumbnailUnavailableSrc } from "../../utils/constants";
import { AddBookRequest, AddBookResponse } from "../../models/AddBook";

const AddModifyBookForm = ({ onClose }: { onClose: () => void }) => {
    const [localBook, setLocalBook] = useState<Book | undefined>(defaultBook);
    const [showGoogleSearchForm, setShowGoogleSearchForm] = useState(false);
    const [searchTitle, setSearchTitle] = useState("");
    const dispatch = useAppDispatch();
    const searchResults = useSelector(selectGoogleSearchBooks);


    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setLocalBook({ ...localBook, [name]: value });
    };

    const handleCategoryChange = (value: BookCategory) => {
        setLocalBook({ ...localBook, category: value });
    };

    const handleSearchSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        dispatch(searchGoogleBooks(searchTitle));
    };

    const handleSearchResultClick = (resultBook: Book) => {
        console.log(resultBook)
        setLocalBook({
            ...resultBook
        });
        setShowGoogleSearchForm(false);
    };

    const handleSaveBook = async () => {
        const saveBookRequest = {
            book: localBook,
        } as AddBookRequest;

        const dispatchResp = await dispatch(addBook(saveBookRequest));
        const addBookResponse = dispatchResp.payload as AddBookResponse;

        console.log(addBookResponse)   
        if (addBookResponse.isSuccessful) {
            alert("Book added successfully.")
            await dispatch(getBooksList);
            onClose();
        } else {
            alert(addBookResponse.errorMessage || "An error occurred.");
        }
    };

    const googleSearchFormJsx = (
        <div>
            <Form onSubmit={handleSearchSubmit} className="googleSearchForm">
                <Form.Group>
                    <Form.Label>Search Google for Title</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter a title and hit enter"
                        value={searchTitle}
                        onChange={(e) => setSearchTitle(e.target.value)}
                    />
                </Form.Group>
            </Form>
            <div className="googleBookResults">
                {searchResults.map((resultBook: Book, index: number) => (
                    <div key={index} className="googleBookResultItem" onClick={() => handleSearchResultClick(resultBook)}>
                        <img src={resultBook.imageUrl ?? bookThumbnailUnavailableSrc} alt={resultBook.title} className="googleSearchThumbnail" title="Click to prepopulate fields." />
                        <div>
                            <div><strong>{resultBook.title}</strong></div>
                            <div>{resultBook.description?.slice(0, 150)}</div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );

    const addBookFormJsx = (
        <div>
            <Form.Group>
                <Form.Label>Title</Form.Label>
                <Form.Control type="text" name="title" value={localBook?.title || ""} onChange={handleInputChange} />
            </Form.Group>

            <Form.Group>
                <Form.Label>Description</Form.Label>
                <Form.Control as="textarea" rows={3} name="description" value={localBook?.description || ""} onChange={handleInputChange} />
            </Form.Group>

            <Form.Group>
                <Form.Label>Author Name</Form.Label>
                <Form.Control type="text" name="authorName" value={localBook?.authorName || ""} onChange={handleInputChange} />
            </Form.Group>

            {/*<Form.Group>*/}
            {/*    <Form.Label>Published Date</Form.Label>*/}
            {/*    <Form.Control type="date" name="publishedDate" value={formatDate()} onChange={handleInputChange} />*/}
            {/*</Form.Group>*/}

            <Form.Group>
                <Form.Label>MSRP</Form.Label>
                <Form.Control type="number" name="msrp" value={localBook?.msrp || ""} onChange={handleInputChange} />
            </Form.Group>

            {/*<Form.Group>*/}
            {/*    <Form.Label>ISBN</Form.Label>*/}
            {/*    <Form.Control type="text" name="isbn" value={localBook?.isbn || ""} onChange={handleInputChange} />*/}
            {/*</Form.Group>*/}

            <Form.Group>
                <Form.Label>Category</Form.Label>
                <Form.Control as="select" name="category" value={localBook?.category || ""} onChange={(e) => handleCategoryChange(e.target.value as unknown as BookCategory)}>
                    {Object.values(BookCategory).map((category, index) => (
                        <option value={category} key={index}>{category}</option>
                    ))}
                </Form.Control>
            </Form.Group>

            {localBook?.category === BookCategory.Other && (
                <Form.Group>
                    <Form.Label>Other Category Name</Form.Label>
                    <Form.Control type="text" name="otherCategoryName" value={localBook?.otherCategoryName || ""} onChange={handleInputChange} />
                </Form.Group>
            )}

            <Form.Group>
                <Form.Label>Image URL</Form.Label>
                <Form.Control type="text" name="imageUrl" value={localBook?.imageUrl || ""} onChange={handleInputChange} />
            </Form.Group>
        </div>
    );

    return (
        <div>
            {showGoogleSearchForm ? googleSearchFormJsx : addBookFormJsx}
            <div className="bookDetailsButtonContainer">
                <Button onClick={() => setShowGoogleSearchForm(!showGoogleSearchForm)}>
                    {showGoogleSearchForm ? "Return to Manual Entry" : "Search Google Books"}
                </Button>
                <Button variant="success" onClick={handleSaveBook}><i className="bi bi-save"></i> Save</Button>
            </div>
        </div>
    );
};

export default AddModifyBookForm;
