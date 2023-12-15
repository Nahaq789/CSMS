import React from "react";
import "../../app/globals.css";
import SearchBar from "../searchbar/SearchBar";
import { Box } from "@mui/material";
const TopBar = () => {
  return (
    <Box className={"top-bar"}>
      <ul>
        <li className={"app-name"}>SAKSAK</li>
        <li className={"search-bar"}>
          <SearchBar />
        </li>
      </ul>
    </Box>
  );
};

export default TopBar;
