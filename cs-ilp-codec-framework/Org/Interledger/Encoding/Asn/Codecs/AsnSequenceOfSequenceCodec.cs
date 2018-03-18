using System;

/*
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnSequenceOfSequenceCodec<L extends List<T>, T> extends AsnObjectCodecBase<L> {

  private final Supplier<AsnSequenceCodec<T>> supplier;
    private final Supplier<L> listSupplier;
    private ArrayList<AsnSequenceCodec<T>> codecs;

    public AsnSequenceOfSequenceCodec(
        Supplier<L> listSupplier,
        Supplier<AsnSequenceCodec<T>> sequenceCodecSupplier)
    {
        this.supplier = sequenceCodecSupplier;
        this.listSupplier = listSupplier;
    }

    public int size()
    {
        Objects.requireNonNull(codecs);
        return this.codecs.size();
    }


    public void setSize(int size)
    {
        this.codecs = new ArrayList<>(size);
        for (int i = 0; i < size; i++)
        {
            this.codecs.add(i, supplier.get());
        }
    }

    public AsnSequenceCodec<T> getCodecAt(int index)
    {
        Objects.requireNonNull(codecs);
        return this.codecs.get(index);
    }

  public L decode()
    {
        Objects.requireNonNull(codecs);
        L list = listSupplier.get();
        for (AsnSequenceCodec<T> codec : codecs)
        {
            list.add(codec.decode());
        }
        return list;
    }

  public void encode(L values)
    {
        this.codecs = new ArrayList<>(values.size());
        for (T value : values)
        {
            AsnSequenceCodec<T> codec = supplier.get();
            codec.encode(value);
            this.codecs.add(codec);
        }
    }

}

}
*/