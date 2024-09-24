import { useState, useEffect } from 'react';
import '../assets/authors.css'
import { Link } from "react-router-dom";
import fetcher from '../Fetcher';

function Authors() {
    const [authors, setAuthors] = useState<Author[]>([])
    const [query, setQuery] = useState<string>('')
    const [filteredAuthors, setFilteredAuthors] = useState<Author[]>([])

    interface Author {
        id : number
        firstName : string
        lastName : string
        imageUrl : string
    }

    // Fetch authors on component mount
    useEffect(() => {
        fetchAuthors();
    }, []);

    // Filter authors when either `authors` or `query` changes
    useEffect(() => {
        filterAuthors();
    }, [authors, query]);  // This ensures it refilters whenever authors or query change

    function fetchAuthors() {
        fetcher<Author[]>('https://localhost:44371/authors')
        .then(data => {
            setAuthors(data);
        });
    }

    function displayAuthors() {
        return filteredAuthors.map(author => (
            <div className='card' key={author.id}>
                <img className='author-img' src={author.imageUrl} alt={author.firstName} />
                <h2 className='author-name' style={{marginTop: '2%'}}>{author.firstName} <br/> {author.lastName}</h2>
                <div className='btn-group'>
                    <Link to={`/authors/${author.id}`}><button className='view-btn btn'>View</button></Link>
                    <Link to={`/authors/form/${author.id}`}><button className='edit-btn btn'>Edit</button></Link>
                    <button className='delete-btn btn' onClick={() => deleteAuthor(author.id)}>Delete</button>
                </div>
            </div>
        ))
    }

    function deleteAuthor(id: number) {
        fetcher(`https://localhost:44371/authors/${id}`, 'DELETE')
        .then(() => fetchAuthors())
    }

    function handleChange(event: any) {
        setQuery(event.target.value);
    }

    // Filter the authors based on the search query
    function filterAuthors() {
        if (query === '') {
            setFilteredAuthors(authors);
        } else {
            setFilteredAuthors(authors.filter(author =>
                (author.firstName + ' ' + author.lastName).toLowerCase().includes(query.toLowerCase())
            ));
        }
    }

    return (
        <div style={{marginTop: '7%'}}>
            <div className='input-container ic1'>
                <input id='search' className='input' type="text" name="query" value={query} onChange={handleChange}/>
                <div className='cut'></div>
                <label className='placeholder'>Search</label>
            </div>
            <h1 style={{marginTop: '2%'}}>Authors</h1>
            <Link to='/authors/form'><button className='add-btn btn'>Add Author</button></Link>
            <div className='card-group'>
                {displayAuthors()}
            </div>
        </div>
    );
}

export default Authors;
