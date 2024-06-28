namespace saucedemotests.models
{
    public struct ItemModel(string title, string description, string price)
    {
        public string Title { set; get; } = title;
        public string Description { set; get; } = description;
        public string Price { set; get; } = price;

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not ItemModel)
            {
                return false;
            }

            ItemModel otherItem = (ItemModel) obj;

            return Title.Equals(otherItem.Title) 
            && Description.Equals(otherItem.Description)
            && Price.Equals(otherItem.Price);
        }
    }
}