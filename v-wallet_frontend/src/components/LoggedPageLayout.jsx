import Header from "./Header";

const LoggedPageLayout = (props) => {
    return <div>
        <Header log={true} />
        {props.children}
    </div>;
}
    

export default LoggedPageLayout;