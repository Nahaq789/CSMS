"use client";
import * as React from "react";
import styles from './searchbar.module.css'

const SearchBar = (): React.JSX.Element => {
    return (
        <input type='text' placeholder='Search...' className={styles.input}/>
    )
}

export default SearchBar;
