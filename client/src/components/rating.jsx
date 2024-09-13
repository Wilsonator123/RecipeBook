import Fstar from "@/assets/fstar.svg";
import Star from "@/assets/star.svg";

export default function Rating({rating}) {

    return (
        <div className="flex ">
            { rating >= 1 ? <Fstar fill="black" width="20" height="20"/> : <Star fill="black" width="20" height="20"/> }
            { rating >= 2 ? <Fstar fill="black" width="20" height="20"/> : <Star fill="black" width="20" height="20"/> }
            { rating >= 3 ? <Fstar fill="black" width="20" height="20"/> : <Star fill="black" width="20" height="20"/> }
            { rating >= 4 ? <Fstar fill="black" width="20" height="20"/> : <Star fill="black" width="20" height="20"/> }
            { rating >= 5 ? <Fstar fill="black" width="20" height="20"/> : <Star fill="black" width="20" height="20"/> }
        </div>
    )
}