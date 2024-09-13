import Close from "@/assets/close.svg";

export default function Tag({value, onClose, index, className}){
    return (
        <div className={`flex rounded-3xl text-white px-1 gap-1 items-center justify-center bg-secondary ${className}`}>
            { onClose && <Close width="15" height="15" onClick={() => onClose(index)}/> }
            <h1>{value}</h1>
        </div>
    )
}