import axios from '../apiConfig'
import {Api} from "@mui/icons-material";

export async function getAllTask() {
    const result = await axios.get("/task");
}