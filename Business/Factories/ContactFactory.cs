using Business.Dtos;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactDto Create()
    {
        return new ContactDto();
    }

}
