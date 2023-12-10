import {ChangeEvent, useState} from "react";
import SearchIcon from '@mui/icons-material/Search';
const SearchBar = () => {
    const [searchQuery, setSearchQuery] = useState('')
    
    return(
        <div className={'searchbar'}>
            <SearchIcon />
            <input
                type={'text'}
                placeholder={'Search for customer, contract...'}
                value={ searchQuery }
                onChange={ (e : ChangeEvent<HTMLInputElement>) => setSearchQuery(e.target.value)}
            />
        </div>
    )
}

export default SearchBar;